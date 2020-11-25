import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { FormsModule, ReactiveFormsModule } from '@angular/forms';

import { AlertModule } from 'ngx-bootstrap/alert';
import { CollapseModule } from 'ngx-bootstrap/collapse';
import { BsDropdownModule } from 'ngx-bootstrap/dropdown';
import { FontAwesomeModule } from '@fortawesome/angular-fontawesome';

import { DefaultRoutingModule } from './default-routing.module';
import { DefaultComponent } from './default.component';
import { NavbarComponent } from './navbar/navbar.component';
import { HomeComponent } from './pages/home/home.component';
import { FeaturesComponent } from './pages/features/features.component';
import { PricingComponent } from './pages/pricing/pricing.component';

@NgModule({
  declarations: [
    DefaultComponent,
    NavbarComponent,
    HomeComponent,
    FeaturesComponent,
    PricingComponent
  ],
  imports: [
    CommonModule,
    DefaultRoutingModule,
    FormsModule,
    ReactiveFormsModule,
    FontAwesomeModule,
    AlertModule.forRoot(),
    CollapseModule.forRoot(),
    BsDropdownModule.forRoot()
  ]
})
export class DefaultModule { }
