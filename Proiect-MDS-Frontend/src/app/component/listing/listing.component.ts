import { CommonModule } from '@angular/common';
import { Component, NgModule, OnInit } from '@angular/core';
import { ActivatedRoute, Router } from '@angular/router';
import { NavbarComponent } from '../navbar/navbar.component';

@Component({
  selector: 'app-listing',
  templateUrl: './listing.component.html',
  styleUrl: './listing.component.css',
})
export class ListingComponent implements OnInit {
  results: any[] = [];

  constructor(private route: ActivatedRoute, private router: Router) {}

  ngOnInit(): void {
    this.results = history.state.results;
    console.log(this.results);
  }}

  @NgModule({
    declarations: [
      ListingComponent
    ],
    imports: [
      CommonModule,
      NavbarComponent,
    ]
  })
  export class ListingComponentModule { }