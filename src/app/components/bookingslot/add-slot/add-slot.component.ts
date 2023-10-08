import { Component, OnInit } from '@angular/core';
import { Router } from '@angular/router';
import { ToastrService } from 'ngx-toastr';
import { BookingSlot } from 'src/app/models/booking-slot.model';
import { BookingslotService } from 'src/app/services/bookingslot.service';

@Component({
  selector: 'app-add-slot',
  templateUrl: './add-slot.component.html',
  styleUrls: ['./add-slot.component.css'],
})
export class AddSlotComponent implements OnInit {
  cities: string[] = ['Bengaluru', 'Hyderabad', 'Pune'];
  vaccineCentres: { [key: string]: string[] } = {
    Bengaluru: ['Vaccine Health Care', 'Baby Vaccine'],
    Pune: ['Late Baburao Genba Shewale Hospital', 'Aundh Kuti Hospital'],
    Hyderabad: ['Medicover Hospitals', 'Apollo Hospitals'],
  };
  vaccines: { [key: string]: string[] } = {
    'Vaccine Health Care': ['Covaccine', 'Covishield', 'Rabivax'],
    'Baby Vaccine': ['Rabivax', 'Tetanus Vaccine'],
    'Late Baburao Genba Shewale Hospital': ['Covishield', 'Rabivax', 'MMRV'],
    'Aundh Kuti Hospital': ['Covaccine', 'Tetanus Vaccine', 'MMRV'],
    'Medicover Hospitals': ['Covaccine', 'Covishield', 'Rabivax'],
    'Apollo Hospitals': ['Covaccine', 'MMRV'],
  };
  selectedCity: string = '';
  selectedCentre: string = '';
  selectedVaccineName: string = '';

  addSlot: BookingSlot = {
    userId: 0,
    firstName: '',
    lastName: '',
    userName: '',
    email: '',
    address: '',
    gender: '',
    dateOfBooking: new Date(),
    city: '',
    vaccineCentre: '',
    vaccineName: '',
  };

  constructor(
    private slotService: BookingslotService,
    private toastr: ToastrService,
    private router: Router
  ) {}

  ngOnInit(): void {}
  addVaccineStock() {
    // Setting city, vaccineCentre, and vaccineName in the addSlot object
    this.addSlot.city = this.selectedCity;
    this.addSlot.vaccineCentre = this.selectedCentre;
    this.addSlot.vaccineName = this.selectedVaccineName;

    console.log(this.addSlot);

    this.slotService.addVaccineStock(this.addSlot).subscribe({
      next: (slotBook) => {
        console.log(slotBook);
        this.toastr.success('Vaccine Added Successfully', 'Success!');
        this.router.navigate(['bookingslot']);
      },
      error: (response: any) => {
        console.log(response);
        this.toastr.error('Failed to fetch data', 'Error!');
      },
    });
  }
}
