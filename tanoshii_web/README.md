# Introduction
This is my portfolio website.

## How to deploy C# app to Heroku
reference: https://dev.to/alrobilliard/deploying-net-core-to-heroku-1lfe

### Frequently used Docker commands
`docker build -t tanoshii_web:0.0.5 .`

`docker run -p 8080:80 --rm tanoshii_web:0.0.5`

#### For Heroku
**The name for app does not contain underscore!**
`heroku container:push -a tanoshii-web web`
`heroku container:release -a tanoshii-web web`