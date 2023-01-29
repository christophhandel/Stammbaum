import {Job} from "./job.model";
import {Company} from "./company.model";

export interface Person {
  id: string|null;
  firstname: string;
  lastname: string;
  sex: string|null;
  jobId: string | null;

  birthLocationId: string| null;
  companyId: string| null;


  fatherId: string|null;
  motherId: string|null;
}
