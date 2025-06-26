import { TestBed } from '@angular/core/testing';
import {
  HttpTestingController,
  provideHttpClientTesting,
} from '@angular/common/http/testing';
import { RecipeService } from './recipe-service';
import { provideHttpClient } from '@angular/common/http';

describe('RecipeService', () => {
  let service: RecipeService;
  let httpMock: HttpTestingController;

  beforeEach(() => {
    TestBed.configureTestingModule({
      imports: [],
      providers: [
        RecipeService,
        provideHttpClient(),
        provideHttpClientTesting(),
      ],
    });

    service = TestBed.inject(RecipeService);
    httpMock = TestBed.inject(HttpTestingController);
  });

  it('should be created', () => {
    expect(service).toBeTruthy();
  });

  it('should fetch recipes', () => {
    const mockResponse = {
      recipes: [
        {
          id: 1,
          name: 'Classic Margherita Pizza',
          cuisine: 'Italian',
          cookTimeMinutes: 15,
          ingredients: [
            'Pizza dough',
            'Tomato sauce',
            'Fresh mozzarella cheese',
            'Fresh basil leaves',
            'Olive oil',
            'Salt and pepper to taste',
          ],
          image: 'https://cdn.dummyjson.com/recipe-images/1.webp',
        },
        {
          id: 2,
          name: 'Vegetarian Stir-Fry',
          cuisine: 'Asian',
          cookTimeMinutes: 20,
          ingredients: [
            'Tofu, cubed',
            'Broccoli florets',
            'Carrots, sliced',
            'Bell peppers, sliced',
            'Soy sauce',
            'Ginger, minced',
            'Garlic, minced',
            'Sesame oil',
            'Cooked rice for serving',
          ],
          image: 'https://cdn.dummyjson.com/recipe-images/2.webp',
        },
        {
          id: 3,
          name: 'Chocolate Chip Cookies',
          cuisine: 'American',
          cookTimeMinutes: 10,
          ingredients: [
            'All-purpose flour',
            'Butter, softened',
            'Brown sugar',
            'White sugar',
            'Eggs',
            'Vanilla extract',
            'Baking soda',
            'Salt',
            'Chocolate chips',
          ],
          image: 'https://cdn.dummyjson.com/recipe-images/3.webp',
        },
      ],
      total: 50,
      skip: 0,
      limit: 3,
    };

    service.getRecipes().subscribe((data: any) => {
      expect(data).toEqual(mockResponse);
    });

    const req = httpMock.expectOne('https://dummyjson.com/recipes');
    expect(req.request.method).toBe('GET');
    req.flush(mockResponse);
  });
});
