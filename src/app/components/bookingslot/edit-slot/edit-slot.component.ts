import { Component, OnInit } from '@angular/core';
import { ActivatedRoute, Router } from '@angular/router';
import { ToastrService } from 'ngx-toastr';
import { BookingSlot } from 'src/app/models/booking-slot.model';
import { BookingslotService } from 'src/app/services/bookingslot.service';

@Component({
  selector: 'app-edit-slot',
  templateUrl: './edit-slot.component.html',
  styleUrls: ['./edit-slot.component.css'],
})
export class EditSlotComponent implements OnInit {
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

  genderOptions: string[] = ['Male', 'Female', 'Other'];
  selectedCity: string = '';
  selectedCentre: string = '';

  bookingDetails: BookingSlot = {
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
    private route: ActivatedRoute,
    private bookingService: BookingslotService,
    private toastr: ToastrService,
    private router: Router
  ) {}

  ngOnInit(): void {
    this.route.paramMap.subscribe({
      next: (params) => {
        const id = params.get('id');
        if (id) {
          //api call for update
          this.bookingService.getVaccine(id).subscribe({
            next: (response) => {
              console.log(response);
              this.bookingDetails = response;
            },
            error: (response: any) => {
              console.log(response);
              this.toastr.error('Failed to fetch data', 'Error!');
            },
          });
        }
      },
    });
  }

  onCityChange() {
    this.bookingDetails.vaccineCentre = '';
    this.bookingDetails.vaccineName = '';
  }

  onCentreChange() {
    this.bookingDetails.vaccineName = '';
  }
  updateVaccine() {
    this.bookingService
      .updateVaccine(this.bookingDetails.userId, this.bookingDetails)
      .subscribe({
        next: (response) => {
          this.toastr.success('Record Updated!', 'Success!');
          this.router.navigate(['bookingslot']);
        },
        error: (response: any) => {
          console.log(response);
          this.toastr.error('Failed to fetch data', 'Error!');
        },
      });
  }

  deleteVaccine(id: number) {
    this.bookingService.deleteVaccine(id).subscribe({
      next: (response) => {
        this.toastr.success('Record Deleted!', 'Success!');
        this.router.navigate(['bookingslot']);
      },
      error: (response: any) => {
        console.log(response);
        this.toastr.error('Failed to fetch data', 'Error!');
      },
    });
  }
}
