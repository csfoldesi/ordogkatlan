import { Component, input } from '@angular/core';
import { Performance } from '../common/models';
import { CommonModule } from '@angular/common';

@Component({
  selector: 'app-performance-item',
  imports: [CommonModule],
  template: `
    <div
      class="bg-white rounded-lg border border-gray-200 shadow-sm hover:shadow-md transition-shadow duration-200 p-6"
    >
      <div class="flex flex-col gap-4">
        <div class="flex-1">
          <h2 class="text-xl font-semibold text-gray-900 mb-2">
            {{ performance().title }}
          </h2>
          <p class="text-gray-600 mb-4 leading-relaxed">
            {{ performance().description }}
          </p>

          <div class="flex flex-wrap gap-4 text-sm text-gray-500 mb-4">
            <div class="flex items-center gap-1">
              <span>{{ performance().startTime | date : 'HH:mm' }}</span>
            </div>
            <div class="flex items-center gap-1">
              <span>{{ performance().villageName }}</span> -
              <span>{{ performance().stageName }}</span>
            </div>
          </div>
        </div>

        <div class="border-t border-gray-100 pt-0">
          <div class="flex flex-wrap gap-1">
            @for( genre of performance().genres; track genre.id) {
            <span
              class="inline-flex items-center px-2.5 py-0.5 rounded-full text-xs font-medium bg-blue-100 text-blue-800"
            >
              {{ genre.name }}
            </span>
            }
          </div>
        </div>
      </div>
    </div>
  `,
  styles: ``,
})
export class PerformanceItem {
  performance = input.required<Performance>();
}
