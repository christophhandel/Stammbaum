import {Location} from "./location.model";
import {Accomplishments} from "./accomplishments.model";

export interface Person {
  id: string|null;
  firstname: string;
  lastname: string;
  sex: string|null;
  jobId: string | null;

  birthLocation: Location| null;
  companyId: string| null;


  fatherId: string|null;
  motherId: string|null;

  accomplishments: Accomplishments[];
}
