import { Component, OnInit } from '@angular/core';
import { AuthService } from '../auth.service';
import { TenantsService } from '../tenants.service';

@Component({
  selector: 'app-profile',
  templateUrl: './profile.component.html',
  styleUrls: ['./profile.component.scss']
})
export class ProfileComponent implements OnInit {
  profile: any;
  tenants: any;

  constructor(
    private authService: AuthService,
    private tenantsService: TenantsService) {}

  async ngOnInit() {
    this.authService.profile.subscribe(profile => this.profile = profile);
    this.tenants = await this.tenantsService.getTenantsMy();
  }
}
