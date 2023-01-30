import { Injectable } from '@angular/core';
import {HttpClient} from "@angular/common/http";
import {Person} from "../models/person.model";
import {environment} from "../../environments/environment";
import {Company} from "../models/company.model";

@Injectable({
  providedIn: 'root'
})
export class RestService {

  constructor(private http: HttpClient) { }


  getPersonByParents(motherId: string, fatherId: string){
    this.http.get<Person[]>(environment.API_URL + "parents?motherId="+ motherId+ "&fatherId=" + fatherId)
  }
  getPersonByMother(motherId: string){
    this.http.get<Person[]>(environment.API_URL + "parents?motherId="+ motherId)
  }

  getPersonByFather(fatherId: string){
    this.http.get<Person[]>(environment.API_URL + "parents?fatherId=" + fatherId)
  }

  getBySex(sex: string){
    return this.http.get<Person[]>(environment.API_URL + "Person?sex=" + sex)
  }

  getAllPeople(){
    return this.http.get<Person[]>(environment.API_URL + "Person");
  }

  getPerson(id: string){
    return this.http.get<Person>(environment.API_URL + "Person/" +id);
  }

  updatePerson(p: Person){
    return this.http.put<Person>(environment.API_URL + p.id, p);
  }

  addPerson(p: Person){
    return this.http.post<Person>(environment.API_URL + "Person", p);
  }

  getCompanys() {
    return this.http.get<Company[]>(environment.API_URL + "Company");
  }
  deletePerson(personId: string){
    return this.http.delete<Person>(environment.API_URL + "person/"+personId)
  }
}
