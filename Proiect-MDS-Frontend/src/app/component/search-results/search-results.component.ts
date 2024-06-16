import { CommonModule } from '@angular/common';
import { Component, NgModule, OnInit } from '@angular/core';
import { ActivatedRoute, Router } from '@angular/router';
import { NavbarComponent } from '../navbar/navbar.component';
import { S3Service } from '../../services/s3.service';

@Component({
  selector: 'app-search-results',
  templateUrl: './search-results.component.html',
  styleUrls: ['./search-results.component.css'],
})
export class SearchResultsComponent implements OnInit {

  results: any[] = [];

  constructor(private route: ActivatedRoute, private router: Router,private s3Service: S3Service) {}
  getUrl(fileImageName: string) {
    return this.s3Service.getObjectUrl('dawbucket', fileImageName);
  }
  ngOnInit(): void {
    this.results = history.state.results;
    console.log(this.results);
  }

  redirectToListingPage(titlu: any): void {
    this.router.navigate(['/listing'], { queryParams: { id: titlu } });
  }  
}

@NgModule({
  declarations: [SearchResultsComponent],
  imports: [CommonModule, NavbarComponent],
  providers: [S3Service],
})
export class SearchResultsModule {

}
