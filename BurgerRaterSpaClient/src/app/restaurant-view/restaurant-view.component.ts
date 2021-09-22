import { OnDestroy } from '@angular/core';
import { Component, OnInit } from '@angular/core';
import { Subscription } from 'rxjs/internal/Subscription';
import { Restaurant } from '../models/restaurant';
import { RestaurantService } from '../services/restaurant.service';


@Component({
  selector: 'app-restaurant-view',
  templateUrl: './restaurant-view.component.html',
  styleUrls: ['./restaurant-view.component.css']
})
export class RestaurantViewComponent implements OnInit, OnDestroy {
  
  restaurants: Restaurant[] = [];
  subscription$: Subscription | undefined;

  constructor(private service: RestaurantService) { }

  ngOnInit(): void {
    this.getRestaurants();
  }

  getRestaurants(): void {
    this.subscription$ = this.service.getRestaurants()
      .subscribe((restaurants: Restaurant[]) => {
        this.restaurants = restaurants;
      });
  }

  ngOnDestroy() {
    if (this.subscription$) {
      this.subscription$.unsubscribe();
    }
    
  }
}
