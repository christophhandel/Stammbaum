import {Job} from "./job.model";
import {Company} from "./company.model";

export interface Person {
  id: string|null;
  firstname: string;
  lastname: string;
  sex: string|null;
  job: Job | null;

  birthLocation: Location | null;
  company: Company | null;


  fatherId: string|null;
  motherId: string|null;
}
