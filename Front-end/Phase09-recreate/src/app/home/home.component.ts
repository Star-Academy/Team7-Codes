import { Component, OnInit } from '@angular/core';
import { SearchService } from './service.service';
import { Document } from '../result/models/document';


@Component({
  selector: 'app-home',
  templateUrl: './home.component.html',
  styleUrls: ['./home.component.scss']
})
export class HomeComponent implements OnInit {

  public documents: Document[];

  constructor(private service: SearchService) { }

  ngOnInit(): void {
  }

  public async searchDocument(query: string) {
    this.documents = await this.service.getDocuments(query);
  }

}


