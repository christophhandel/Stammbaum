import { ComponentFixture, TestBed } from '@angular/core/testing';

import { DescendantsComponent } from './descendants.component';

describe('DescendantsComponent', () => {
  let component: DescendantsComponent;
  let fixture: ComponentFixture<DescendantsComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ DescendantsComponent ]
    })
    .compileComponents();

    fixture = TestBed.createComponent(DescendantsComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
