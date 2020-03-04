# SimpleCarApi
 
 
## Содержание

<!-- toc --> 
- [Routes](#routes) 
- [License](#license)
<!-- tocstop -->
 
## Routes

#### Create car
```
POST /api/cars
```
Пример POST запроса:
```
{ 
  "Name": "Foo",
  "Description": "Bar"
}
```
#### Read car
Пример GET запроса:
```
GET /api/cars/1
```
#### Update car
Пример POST запроса:
```
POST /api/cars
```
```
{
  "Id": "some id1",
  "Name": "new name"
}
```
```
{
  "Id": "some id2",
  "Description": "null" // reset current desc to null
}
```
#### Delete car
Пример DELETE запроса:
```
DELETE /api/cars/1
```


## License
MIT


