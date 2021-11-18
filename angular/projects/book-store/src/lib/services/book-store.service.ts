import { Injectable } from '@angular/core';
import { RestService } from '@abp/ng.core';

@Injectable({
  providedIn: 'root',
})
export class BookStoreService {
  apiName = 'BookStore';

  constructor(private restService: RestService) {}

  sample() {
    return this.restService.request<void, any>(
      { method: 'GET', url: '/api/BookStore/sample' },
      { apiName: this.apiName }
    );
  }
}
