import { Component, OnInit } from '@angular/core';
import { ChartOptions } from 'chart.js';
import { UserStoreService } from 'src/app/services/user-store.service';
import { AuthService } from 'src/app/services/auth.service';

@Component({
  selector: 'app-reports',
  templateUrl: './reports.component.html',
  styleUrls: ['./reports.component.css'],
})
export class ReportsComponent implements OnInit {
  public role: string = '';

  vaccines = [
    {
      name: 'Covaccine',
      jan: 1200,
      feb: 1300,
      mar: 1400,
      apr: 1500,
      may: 1250,
      jun: 1350,
    },
    {
      name: 'Covishield',
      jan: 1500,
      feb: 1400,
      mar: 1300,
      apr: 1200,
      may: 1600,
      jun: 1700,
    },
    {
      name: 'Rabivax',
      jan: 1100,
      feb: 1000,
      mar: 1100,
      apr: 1050,
      may: 1030,
      jun: 1080,
    },
    {
      name: 'Tetanus Vaccine',
      jan: 900,
      feb: 920,
      mar: 880,
      apr: 870,
      may: 890,
      jun: 910,
    },
    {
      name: 'MMRV',
      jan: 700,
      feb: 730,
      mar: 750,
      apr: 720,
      may: 710,
      jun: 725,
    },
  ];

  constructor(private userStore: UserStoreService, private auth: AuthService) {}

  ngOnInit() {
    this.userStore.getRoleFromStore().subscribe((val) => {
      const rolefromToken = this.auth.getRoleFromToken();
      this.role = val || rolefromToken;
    });
  }

  showFullReport = true;
  searchTerm = '';

  toggleReport() {
    this.showFullReport = !this.showFullReport;
  }

  filterVaccines() {
    return this.vaccines.filter((vaccine) =>
      vaccine.name.toLowerCase().includes(this.searchTerm.toLowerCase())
    );
  }

  get totalVaccines() {
    return this.vaccines.map((v) => {
      return {
        name: v.name,
        total: v.jan + v.feb + v.mar + v.apr + v.may + v.jun,
      };
    });
  }

  public pieChartOptions: ChartOptions = {
    responsive: true,
  };
  public pieChartLegend = true;
  public pieChartPlugins = [];

  public pieChartLabels = this.totalVaccines.map((v) => v.name);
  public pieChartData: { data: number[] }[] = [
    { data: this.totalVaccines.map((v) => v.total) },
  ];
  public pieChartType = 'pie' as const;
}
