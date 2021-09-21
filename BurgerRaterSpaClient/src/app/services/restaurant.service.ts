import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { protectedResources } from '../auth-config';
import { Restaurant } from '../models/restaurant';

@Injectable({
  providedIn: 'root'
})
export class RestaurantService {
  url = protectedResources.burgerRaterApi.endpoint + 'Restaurants';

  constructor(private http: HttpClient) { }

  getRestaurants() {
    return this.http.get<Restaurant[]>(this.url);
  }

  getRestaurant(id: string) {
    return this.http.get<Restaurant>(this.url + '/' + id);
  }

  postRestaurant(restaurant: Restaurant) {
    return this.http.post<Restaurant>(this.url, restaurant);
  }

  deleteRestaurant(id: number) {
    return this.http.delete(this.url + '/' + id);
  }

  editRestaurant(restaurant: Restaurant) {
    return this.http.put<Restaurant>(this.url + '/' + restaurant.id, restaurant);
  }
}
