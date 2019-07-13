import { NgModule } from '@angular/core';
import { Routes, RouterModule } from '@angular/router';
import { HomeComponent } from './pages/home/home.component';
import { CallbackComponent } from './pages/callback/callback.component';

const routes: Routes = [
  {
    path: '',
    component: HomeComponent
  },
  {
    path: 'callback',
    component: CallbackComponent
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
