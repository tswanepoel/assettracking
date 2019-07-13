import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';

import { DefaultComponent } from './default.component';
import { HomeComponent } from './pages/home/home.component';
import { CallbackComponent } from './pages/callback/callback.component';

const routes: Routes = [
  {
    path: '',
    component: DefaultComponent,
    children: [
      {
        path: '',
        component: HomeComponent
      },
      {
        path: 'callback',
        component: CallbackComponent
      }
    ]
  }
];

@NgModule({
  exports: [ RouterModule ],
  imports: [ RouterModule.forChild(routes) ]
})
export class DefaultRoutingModule { }
