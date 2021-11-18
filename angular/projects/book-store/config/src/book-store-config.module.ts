import { ModuleWithProviders, NgModule } from '@angular/core';
import { BOOK_STORE_ROUTE_PROVIDERS } from './providers/route.provider';

@NgModule()
export class BookStoreConfigModule {
  static forRoot(): ModuleWithProviders<BookStoreConfigModule> {
    return {
      ngModule: BookStoreConfigModule,
      providers: [BOOK_STORE_ROUTE_PROVIDERS],
    };
  }
}
