import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';

import { AssetTrackingRoutingModule } from './asset-tracking-routing.module';
import { AssetTrackingComponent } from './asset-tracking.component';
import { DashboardComponent } from './pages/dashboard/dashboard.component';

@NgModule({
  declarations: [
    AssetTrackingComponent,
    DashboardComponent
  ],
  imports: [
    CommonModule,
    AssetTrackingRoutingModule
  ]
})
export class AssetTrackingModule { }
