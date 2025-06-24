import { HttpClient, httpResource } from '@angular/common/http';
import { computed, inject, Injectable, signal } from '@angular/core';
import { ApiResponse, Catalog } from '../common/models';
import { environment } from '../../environments/environment';

export type SearchParams = {
  dates: string[];
  villages: string[];
  stages: string[];
  genres: string[];
};

@Injectable({ providedIn: 'root' })
export class SearchDataSource {
  httpClient = inject(HttpClient);

  params = signal<SearchParams>({
    dates: [],
    villages: [],
    stages: [],
    genres: [],
  });

  private catalogData = httpResource<ApiResponse<Catalog>>(
    () => `${environment.apiUrl}/catalog`
  );

  catalog = computed(
    () =>
      this.catalogData.value()?.data ?? {
        villages: [],
        stages: [],
        genres: [],
        dates: [],
      }
  );

  selectedVillages = computed(() => {
    return this.catalog().villages.filter((village) =>
      this.params().villages.includes(village.id)
    );
  });

  selectedDatesChanged = (values: string[]) => {
    this.params.update((params) => ({
      ...params,
      dates: values,
    }));
  };

  // ToDo: deselect stages if village is deselected
  selectedVillagesChanged = (values: string[]) => {
    this.params.update((params) => ({
      ...params,
      villages: values,
    }));
  };

  selectedStagesChanged = (values: string[]) => {
    this.params.update((params) => ({
      ...params,
      stages: values,
    }));
  };
}
