# Portfolio João Pedro 

This is my personal portfolio using mong db and available in an azure repos.

<p align="center">
  <img alt="GitHub language count" src="https://img.shields.io/github/languages/count/joomoraes/me-portfolio_mongodb_aspnet_web">

  <img alt="Repository size" src="https://img.shields.io/github/repo-size/joomoraes/me-portfolio_mongodb_aspnet_web">
  
  <a href="https://github.com/joaoritter8/creamtGestaoBiblioteca/commits/master">
    <img alt="GitHub last commit" src="https://img.shields.io/github/last-commit/joomoraes/me-portfolio_mongodb_aspnet_web">
  </a>
  <img alt="Packagist" src="https://img.shields.io/badge/License-MIT-green.svg">
</p>


## Usage

``` yaml
name: Portfolio of Projects 

on:
  projects_pull:
  
 jobs:
  portfolio-plan:
    runs-on: azure-repo

 steps:
      - uses: development/implementation
      - uses: implementation/releaseV1
        with:
          portfolio_wrapper: false
      - run: web-repository    
      - run: repository plan -lock=false -out repository.plan
      
      # generate plain output
      - run: portfolio show -no-color portfolio.plan > portfolio.text
      
       # generate json output
      - run: portfolio show -json portfolio.plan > portfolio.json
      
      - uses: joomoraes/action-portfolio-report@v2
       with:
          # tell the action the plan outputs
          portfolio-text: ${{ github.workspace }}/portfolio.text
          portfolio-json: ${{ github.workspace }}/portfolio.json
          remove-stale-reports: true
```

> **Note**: Ensure `portfolio_wrapper` is set to `false` to better capture the output into a file *(or use your own method)*

