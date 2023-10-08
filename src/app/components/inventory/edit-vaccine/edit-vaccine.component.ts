import { Component, OnInit } from '@angular/core';
import { ActivatedRoute, Router } from '@angular/router';
import { ToastrService } from 'ngx-toastr';
import { VaccineStock } from 'src/app/models/stock-vaccine.model';
import { StockvaccineService } from 'src/app/services/stockvaccine.service';

@Component({
  selector: 'app-edit-vaccine',
  templateUrl: './edit-vaccine.component.html',
  styleUrls: ['./edit-vaccine.component.css'],
})
export class EditVaccineComponent implements OnInit {
  vaccineDetails: VaccineStock = {
    id: 0,
    vaccineName: '',
    manufacturer: '',
    expiryDate: new Date(),
    stock: 0,
  };

  constructor(
    private route: ActivatedRoute,
    private vaccineService: StockvaccineService,
    private toastr: ToastrService,
    private router: Router
  ) {}

  ngOnInit(): void {
    this.route.paramMap.subscribe({
      next: (params) => {
        const id = params.get('id');
        if (id) {
          //api call for update
          this.vaccineService.getVaccine(id).subscribe({
            next: (response) => {
              console.log(response);
              this.vaccineDetails = response;
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

  updateVaccine() {
    this.vaccineService
      .updateVaccine(this.vaccineDetails.id, this.vaccineDetails)
      .subscribe({
        next: (response) => {
          this.toastr.success('Record Updated!', 'Success!');
          this.router.navigate(['inventory']);
        },
        error: (response: any) => {
          console.log(response);
          this.toastr.error('Failed to fetch data', 'Error!');
        },
      });
  }

  deleteVaccine(id: number) {
    this.vaccineService.deleteVaccine(id).subscribe({
      next: (response) => {
        this.toastr.success('Record Deleted!', 'Success!');
        this.router.navigate(['inventory']);
      },
      error: (response: any) => {
        console.log(response);
        this.toastr.error('Failed to fetch data', 'Error!');
      },
    });
  }
}
