import { BrowserModule } from '@angular/platform-browser';
import { NgModule } from '@angular/core';
import { FormsModule } from '@angular/forms';
import { AppRoutingModule } from './app-routing.module';
import { AppComponent } from './app.component';
import { HomeComponent } from './home/home.component';
import { SearchCardComponent } from './home/search-card/search-card.component';
import { TitleComponent } from './home/title/title.component';
import { CreditComponent } from './credit/credit.component';
import { ResultComponent } from './result/result.component';
import { SingleResultComponent } from './result/single-result/single-result.component';


@NgModule({
  declarations: [
    AppComponent,
    HomeComponent,
    SearchCardComponent,
    TitleComponent,
    CreditComponent,
    ResultComponent,
    SingleResultComponent,
  ],
  imports: [
    BrowserModule,
    AppRoutingModule,
    FormsModule
  ],
  providers: [],
  bootstrap: [AppComponent]
})
export class AppModule { }
