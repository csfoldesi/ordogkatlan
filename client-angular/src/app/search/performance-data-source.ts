import {
  computed,
  effect,
  inject,
  Injectable,
  Signal,
  signal,
} from '@angular/core';
import { PagedApiResponse, Performance } from '../common/models';
import { HttpClient } from '@angular/common/http';
import { SearchParams } from './search-data-source';
import {
  takeUntilDestroyed,
  toObservable,
  toSignal,
} from '@angular/core/rxjs-interop';
import { debounceTime, switchMap, tap } from 'rxjs';
import { environment } from '../../environments/environment';

@Injectable({ providedIn: 'root' })
export class PerformanceDataSource {
  private httpClient = inject(HttpClient);

  private searchParams: Signal<SearchParams> | null = null;
  private pageNumber = signal(0);
  private pageSize = signal(10);

  performances = signal<Performance[]>([]);
  hasMorePages = signal(true);

  private trigger = computed(() => ({
    params: this.searchParams ? this.searchParams() : null,
    page: {
      number: this.pageNumber(),
      size: this.pageSize(),
    },
  }));

  performancesData = toSignal(
    toObservable(this.trigger).pipe(
      debounceTime(300),
      switchMap(({ params, page }) =>
        this.httpClient
          .get<PagedApiResponse<Performance[]>>(
            `${environment.apiUrl}/performances`,
            {
              params: {
                ...params,
                pageNumber: page.number,
                pageSize: page.size,
              },
            }
          )
          .pipe(
            tap((response) => {
              this.pageNumber.set(response.currentPage);
              this.pageSize.set(response.pageSize);
              this.hasMorePages.set(
                response.currentPage < response.totalPages - 1
              );
              this.performances.update((current) => [
                ...current,
                ...response.data,
              ]);
            })
          )
      ),
      takeUntilDestroyed()
    ),
    { initialValue: undefined }
  );

  constructor() {
    effect(() => {
      console.log('performances', this.performances());
    });
  }

  initialize(searchParams: Signal<SearchParams>) {
    this.searchParams = searchParams;
  }

  resetData = () => {
    this.pageNumber.set(0);
    this.performances.set([]);
  };

  loadMore = () => {
    this.pageNumber.set(this.pageNumber() + 1);
  };
}
