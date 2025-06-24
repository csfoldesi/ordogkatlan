import { Component } from '@angular/core';
import Search from '../search/search';

@Component({
  selector: 'app-home',
  imports: [Search],
  template: `<app-search />`,
  styles: ``,
})
export default class Home {}
