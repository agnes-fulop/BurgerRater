import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { protectedResources } from '../auth-config';
import { Restaurant } from '../models/restaurant';
import { Review } from '../models/review';

@Injectable({
  providedIn: 'root'
})
export class ReviewService {
  url = protectedResources.burgerRaterApi.endpoint + 'Restaurants/';

  constructor(private http: HttpClient) { }

  getReviewsForRestaurant(restaurantId: string) {
    return this.http.get<Review[]>(this.url + restaurantId + '/Reviews');
  }

  postReview(restaurantId: string, review: Review) {
    return this.http.post<Restaurant>(this.url + restaurantId + '/Reviews', review);
  }

}
