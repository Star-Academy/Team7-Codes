import { Component, OnInit, Input } from '@angular/core';
import { Document } from '../models/document';

@Component({
  selector: 'app-single-result',
  templateUrl: './single-result.component.html',
  styleUrls: ['./single-result.component.scss']
})
export class SingleResultComponent implements OnInit {

  @Input()
  public document: Document;

  constructor() { }

  ngOnInit(): void {
  }

}
