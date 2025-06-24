import { Routes } from '@angular/router';

export const routes: Routes = [
  { path: '', redirectTo: 'home', pathMatch: 'full' },
  {
    path: '',
    loadComponent: () => import('./home/home'),
  },
  {
    path: 'search',
    loadComponent: () => import('./search/search'),
  },
];
