export class Person {
  constructor(
    public id: string|null,
    public firstname: string,
    public lastname: string,
    public sex: string|null,
    public fatherId: string|null,
    public motherId: string|null
  ) {}
}
