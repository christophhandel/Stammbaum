import {Component, HostListener, OnInit} from '@angular/core';
import {ActivatedRoute, Router} from "@angular/router";
import {RestService} from "../../services/rest.service";
import {Person} from "../../models/person.model";
import {ToastrService} from "ngx-toastr";

@Component({
  selector: 'app-person-detail',
  templateUrl: './person-detail.component.html',
  styleUrls: ['./person-detail.component.css']
})
export class PersonDetailComponent implements OnInit {
  type: string = "overview";
  view: [number, number] = [10, 10];
  isEditing: boolean = true;
  ancestors: Person[] = [];
  descendants: Person[] = [];
  selectedPerson: Person = {
    id: '',
    firstname: '',
    lastname: '',
    motherId: null,
    fatherId: null,
    sex: null,
    jobId: null,
    birthLocation: {id: null, city: "", country: ""},
    companyId: null
  };


  constructor(private activatedRoute: ActivatedRoute,
              private router: Router,
              private toastr: ToastrService,
              private restService: RestService) {
  }

  ngOnInit(): void {
    if (this.router.url.split("/")[2] == "add") {
      this.isEditing = false;
    } else  {

      // get current Person if there is one
      this.activatedRoute.params.subscribe(params => {
          if (params["id"]) {
            this.restService.getPerson(params["id"]).subscribe(
              {
                next: (value) => {
                  this.selectedPerson = value
                  this.getScreenSize()
                  this.getRelations();
                },
                error: (err) => {
                  this.toastr.error("Couldn't get person!")
                }
              }
            );
          }
        },
      );
    }
  }

  getRelations() {
    if (this.selectedPerson.id != null){
      this.restService.getAncestors(this.selectedPerson.id).subscribe({
        next: value => {
          this.ancestors = value;
          this.getScreenSize();
        }
      });

      this.restService.getDescendants(this.selectedPerson.id).subscribe({
        next: value => {
          this.descendants = value;
          this.getScreenSize();
        }
      });
    }
  }

  @HostListener('window:resize', ['$event'])
  getScreenSize(event?: any) {
    this.view = [window.innerWidth * 0.5, window.innerHeight * 0.75];
  }

  getTitleString() {
    return this.isEditing ? 'Edit ' + this.selectedPerson.firstname + ' ' + this.selectedPerson.lastname : 'Add Person'
  }
}
