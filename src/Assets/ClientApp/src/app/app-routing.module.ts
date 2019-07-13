import { NgModule } from '@angular/core';
import { Routes, RouterModule } from '@angular/router';

const routes: Routes = [
  {
    path: '',
    loadChildren: () => import('./modules/default/default.module').then(m => m.DefaultModule)
  },
  {
    path: 'admin',
    loadChildren: () => import('./modules/administration/administration.module').then(m => m.AdministrationModule)
  },
  {
    path: ':tenant',
    loadChildren: () => import('./modules/asset-tracking/asset-tracking.module').then(m => m.AssetTrackingModule)
  }
];

@NgModule({
  imports: [RouterModule.forRoot(routes)],
  exports: [RouterModule]
})
export class AppRoutingModule { }
