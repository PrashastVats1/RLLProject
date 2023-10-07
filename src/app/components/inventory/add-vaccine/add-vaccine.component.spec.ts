import { ComponentFixture, TestBed } from '@angular/core/testing';

import { AddVaccineComponent } from './add-vaccine.component';

describe('AddVaccineComponent', () => {
  let component: AddVaccineComponent;
  let fixture: ComponentFixture<AddVaccineComponent>;

  beforeEach(() => {
    TestBed.configureTestingModule({
      declarations: [AddVaccineComponent]
    });
    fixture = TestBed.createComponent(AddVaccineComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
