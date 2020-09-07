import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Document } from '../result/models/document';

@Injectable()
export class SearchService {
  constructor(private http: HttpClient) {
  }

  public async getDocuments(searchkey: string): Promise<Document[]> {
    return new Promise<Document[]>((resolve) => {
      this.http.get('https://localhost:5000/search/' + searchkey).subscribe((result: Document[]) => {
        resolve(result);
      });
    });
  }
}