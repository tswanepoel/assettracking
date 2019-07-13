import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { AssetTrackingComponent } from './asset-tracking.component';
import { DashboardComponent } from './pages/dashboard/dashboard.component';

const routes: Routes = [
  {
    path: '',
    component: AssetTrackingComponent,
    children: [
      {
        path: '',
        component: DashboardComponent
      }
    ]
  }
];

@NgModule({
  exports: [ RouterModule ],
  imports: [ RouterModule.forChild(routes) ]
})
export class AssetTrackingRoutingModule { }
