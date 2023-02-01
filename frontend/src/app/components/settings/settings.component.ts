import {Component, OnInit} from '@angular/core';
import {RestService} from "../../services/rest.service";
import {ToastrService} from "ngx-toastr";

@Component({
  selector: 'app-settings',
  templateUrl: './settings.component.html',
  styleUrls: ['./settings.component.css']
})
export class SettingsComponent implements OnInit {

  constructor(private restService: RestService,
              private toastr: ToastrService) {
  }

  ngOnInit(): void {
  }

  generateTestData() {
    this.restService.loadTestDataIntoDb().subscribe({
      next: value => {
        this.toastr.success("Loaded Test Data into Database!")
      }
    })
  }
}
