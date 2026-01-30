import { Routes } from '@angular/router';

export const routes: Routes = [
  {
    path: '',
    loadComponent: () => import('./components/home/home.component').then(m => m.HomeComponent)
  },
  {
    path: 'blog',
    loadComponent: () =>
      import('./components/blog/blog.component')
        .then(m => m.BlogComponent)
  },
  {
    path: 'blog/:post',
    loadComponent: () => import('./components/blog/blog.component')
      .then(m => m.BlogComponent)
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