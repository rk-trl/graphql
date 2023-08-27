# graphql
Intro to GraphQL

3 sections
* GraphQuery
* GaphType
* Schema

Dependencies
1. GraphQL
2. GraphQL.Server.Transports.AspNetCore.SystemTextJson
3. GraphQL.Server.Ui.Altair

To View GraphQL UI:
https://localhost:7093/ui/altair

Post Request: https://localhost:7093/graphql

Query Request: 

Example 1: 
query:

query{
  products{
    id,
    name
  },
  product(id:2){
    name,
    quantity
  },
  product3: product(id:3){
    name
  }
}


Example 2:
query:

query($id: Int){
  product(id:$id){
    id,
    name,
    quantity
  }
}

graphql variable:

{"id":4}