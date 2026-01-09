import { Routes } from '@angular/router';

export const routes: Routes = [
  {
    path: '',
    loadComponent: () => import('./components/home/home.component').then(m => m.HomeComponent)
  },
  {
    path: 'careers/:id',
    loadComponent: () => import('./components/careers/career-detail/career-detail.component')
      .then(m => m.CareerDetailComponent)
  },
  {
    path: '**',
    redirectTo: ''
  }
];