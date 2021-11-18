import { Component, OnInit } from '@angular/core';
import { BookStoreService } from '../services/book-store.service';

@Component({
  selector: 'lib-book-store',
  template: ` <p>book-store works!</p> `,
  styles: [],
})
export class BookStoreComponent implements OnInit {
  constructor(private service: BookStoreService) {}

  ngOnInit(): void {
    this.service.sample().subscribe(console.log);
  }
}
