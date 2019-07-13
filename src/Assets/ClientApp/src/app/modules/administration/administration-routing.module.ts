import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { AuthGuard } from 'src/app/shared/auth.guard';
import { ProfileComponent } from './pages/profile/profile.component';

const routes: Routes = [
  {
    path: 'profile',
    component: ProfileComponent,
    canActivate: [ AuthGuard ]
  }
];

@NgModule({
  exports: [ RouterModule ],
  imports: [ RouterModule.forChild(routes) ]
})
export class AdministrationRoutingModule { }
