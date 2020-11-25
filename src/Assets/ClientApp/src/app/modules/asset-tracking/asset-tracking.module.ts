import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { FormsModule, ReactiveFormsModule } from '@angular/forms';

import { AlertModule } from 'ngx-bootstrap/alert';
import { CollapseModule } from 'ngx-bootstrap/collapse';
import { BsDropdownModule } from 'ngx-bootstrap/dropdown';
import { FontAwesomeModule } from '@fortawesome/angular-fontawesome';

import { AssetTrackingRoutingModule } from './asset-tracking-routing.module';
import { AssetTrackingComponent } from './asset-tracking.component';
import { NavbarComponent } from './navbar/navbar.component';
import { DashboardComponent } from './pages/dashboard/dashboard.component';

@NgModule({
  declarations: [
    AssetTrackingComponent,
    NavbarComponent,
    DashboardComponent
  ],
  imports: [
    CommonModule,
    AssetTrackingRoutingModule,
    FormsModule,
    ReactiveFormsModule,
    FontAwesomeModule,
    AlertModule.forRoot(),
    CollapseModule.forRoot(),
    BsDropdownModule.forRoot()
  ]
})
export class AssetTrackingModule { }
