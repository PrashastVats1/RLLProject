import { AuthService } from 'src/app/services/auth.service';
import { ApiService } from './../../services/api.service';
import { Component, OnInit } from '@angular/core';
import { UserStoreService } from 'src/app/services/user-store.service';

@Component({
  selector: 'app-navbar',
  templateUrl: './navbar.component.html',
  styleUrls: ['./navbar.component.css'],
})
export class NavbarComponent implements OnInit {
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

    this.userStore.getFullNameFromStore().subscribe((val) => {
      const fullNameFromToken = this.auth.getFullNameFromToken();
      this.fullName = val || fullNameFromToken;
    });
  }
  logout() {
    this.auth.signOut();
  }
}
