import { Component, OnInit } from '@angular/core';
import { ActivatedRoute, Router } from '@angular/router';
import { ToastrService } from 'ngx-toastr';
import { DistributionCentre } from 'src/app/models/distribution-center.model';
import { DistributioncentreService } from 'src/app/services/distributioncentre.service';

@Component({
  selector: 'app-edit-center',
  templateUrl: './edit-center.component.html',
  styleUrls: ['./edit-center.component.css'],
})
export class EditCenterComponent implements OnInit {
  centreDetails: DistributionCentre = {
    centerId: 0,
    centreName: '',
    city: '',
  };

  constructor(
    private route: ActivatedRoute,
    private centreService: DistributioncentreService,
    private toastr: ToastrService,
    private router: Router
  ) {}

  ngOnInit(): void {
    this.route.paramMap.subscribe({
      next: (params) => {
        const id = params.get('id');
        if (id) {
          //api call for update
          this.centreService.getCenter(id).subscribe({
            next: (response) => {
              console.log(response);
              this.centreDetails = response;
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
    this.centreService
      .updateCenter(this.centreDetails.centerId, this.centreDetails)
      .subscribe({
        next: (response) => {
          this.toastr.success('Record Updated!', 'Success!');
          this.router.navigate(['distribution']);
        },
        error: (response: any) => {
          console.log(response);
          this.toastr.error('Failed to fetch data', 'Error!');
        },
      });
  }

  deleteVaccine(id: number) {
    this.centreService.deleteCenter(id).subscribe({
      next: (response) => {
        this.toastr.success('Record Deleted!', 'Success!');
        this.router.navigate(['distribution']);
      },
      error: (response: any) => {
        console.log(response);
        this.toastr.error('Failed to fetch data', 'Error!');
      },
    });
  }
}
