import { NgModule, NgModuleFactory, ModuleWithProviders } from '@angular/core';
import { CoreModule, LazyModuleFactory } from '@abp/ng.core';
import { ThemeSharedModule } from '@abp/ng.theme.shared';
import { BookStoreComponent } from './components/book-store.component';
import { BookStoreRoutingModule } from './book-store-routing.module';

@NgModule({
  declarations: [BookStoreComponent],
  imports: [CoreModule, ThemeSharedModule, BookStoreRoutingModule],
  exports: [BookStoreComponent],
})
export class BookStoreModule {
  static forChild(): ModuleWithProviders<BookStoreModule> {
    return {
      ngModule: BookStoreModule,
      providers: [],
    };
  }

  static forLazy(): NgModuleFactory<BookStoreModule> {
    return new LazyModuleFactory(BookStoreModule.forChild());
  }
}
