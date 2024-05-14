import { CommonModule } from '@angular/common';
import { Component, NgModule, OnInit } from '@angular/core';
import { ActivatedRoute, Router } from '@angular/router';
import { NavbarComponent } from '../navbar/navbar.component';

@Component({
  selector: 'app-search-results',
  templateUrl: './search-results.component.html',
  styleUrls: ['./search-results.component.css'],
})
export class SearchResultsComponent implements OnInit {
  results: any[] = [];

  constructor(private route: ActivatedRoute, private router: Router) {}

  ngOnInit(): void {
    this.results = history.state.results;
    console.log(this.results);
  }

  redirectToListingPage(titlu: any): void {
    this.router.navigate(['/listing', titlu], { state: { results: this.results } });
  }  
}

@NgModule({
  declarations: [SearchResultsComponent],
  imports: [CommonModule, NavbarComponent],
})
export class SearchResultsModule {}
