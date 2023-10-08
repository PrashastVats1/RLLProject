import { AuthService } from 'src/app/services/auth.service';
import { ApiService } from './../../services/api.service';
import { Component, OnInit } from '@angular/core';
import { UserStoreService } from 'src/app/services/user-store.service';

@Component({
  selector: 'app-dashboard',
  templateUrl: './dashboard.component.html',
  styleUrls: ['./dashboard.component.css'],
})
export class DashboardComponent implements OnInit {
  public users: any = [];
  public role!: string;

  public fullName: string = '';

  constructor(
    private api: ApiService,
    private auth: AuthService,
    private userStore: UserStoreService
  ) {}
  ngOnInit() {
    this.api.getUsers().subscribe((res) => {
      console.log('API Response:', res);
      this.users = res;
    });

    this.userStore.getRoleFromStore().subscribe((val) => {
      const rolefromToken = this.auth.getRoleFromToken();
      this.role = val || rolefromToken;
    });
  }
}
