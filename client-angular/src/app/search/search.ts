import { Component, inject } from '@angular/core';
import { MatButtonToggleModule } from '@angular/material/button-toggle';
import { MatButtonModule } from '@angular/material/button';
import { CommonModule } from '@angular/common';
import { SearchDataSource } from './search-data-source';
import { PerformanceDataSource } from './performance-data-source';

@Component({
  selector: 'app-search',
  imports: [CommonModule, MatButtonToggleModule, MatButtonModule],
  template: `
    <section>
      <mat-button-toggle-group
        name="dates"
        aria-label="Dates"
        multiple
        (change)="selectedDatesChanged($event.value)"
      >
        @for(date of catalog().dates; track date) {
        <mat-button-toggle value="{{ date }}">{{
          date | date : 'EEEE (MMM d.)' : undefined : 'hu'
        }}</mat-button-toggle>
        }
      </mat-button-toggle-group>
    </section>

    <section>
      <mat-button-toggle-group
        name="villages"
        aria-label="Villages"
        multiple
        (change)="selectedVillagesChanged($event.value)"
      >
        @for(village of catalog().villages; track village.id) {
        <mat-button-toggle value="{{ village.id }}">{{
          village.name
        }}</mat-button-toggle>
        }
      </mat-button-toggle-group>
    </section>

    <section>
      @for(village of selectedVillages(); track village.id) {
      <mat-button-toggle-group
        name="villages"
        aria-label="Villages"
        multiple
        (change)="selectedStagesChanged($event.value)"
      >
        @for(stage of village.stages; track stage.id) {
        <mat-button-toggle value="{{ stage.id }}">{{
          stage.name
        }}</mat-button-toggle>
        }
      </mat-button-toggle-group>
      }
    </section>
    <section>
      <button matButton (click)="loadMore()" [disabled]="!hasMorePages()">
        Next
      </button>
    </section>
  `,
  styles: ``,
})
export default class Search {
  searchDataSource = inject(SearchDataSource);
  performancesDataSource = inject(PerformanceDataSource);

  catalog = this.searchDataSource.catalog;
  selectedVillages = this.searchDataSource.selectedVillages;

  performances = this.performancesDataSource.performances;

  selectedDatesChanged = (values: string[]) => {
    this.performancesDataSource.resetData();
    this.searchDataSource.selectedDatesChanged(values);
  };

  selectedVillagesChanged = (values: string[]) => {
    this.performancesDataSource.resetData();
    this.searchDataSource.selectedVillagesChanged(values);
  };

  selectedStagesChanged = (values: string[]) => {
    this.performancesDataSource.resetData();
    this.searchDataSource.selectedStagesChanged(values);
  };

  loadMore = this.performancesDataSource.loadMore;
  hasMorePages = this.performancesDataSource.hasMorePages;

  constructor() {
    this.performancesDataSource.initialize(this.searchDataSource.params);
  }
}
