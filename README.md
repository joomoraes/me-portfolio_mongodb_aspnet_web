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

### Inputs

| input      | required | default      | description                                                      |
|------------|----------|--------------|------------------------------------------------------------------|
| `config`   | ❌       | [see docs][] | File path to configuration file                                  |
| `dry`      | ❌       | `false`      | Execute in "dry-run" mode                                        |
| `debug`    | ❌       | `false`      | Output debugging information                                     |
| `format`   | ❌       | [see docs][] | The Git tag format used by semantic-release to identify releases |
| `branches` | ❌       | [see docs][] | The branches on which releases should happen                     |

> ⚠️ ***Note**: only use `config` if you're using a non-standard [configuration file name][`semantic-release` configuration file] and in **JSON format***

### Outputs

| output                               | example  | description                                                                                                                     |
|--------------------------------------|----------|---------------------------------------------------------------------------------------------------------------------------------|
| `published`                          | `true`   | `'true'` when release is successfully published, `'false'` when nothing is published                                            |
| `last-release-git-head`              | `d80709` | The sha of the last commit being part of the last release                                                                       |
| `last-release-git-tag`               | `v1.0.0` | The Git tag associated with the last release                                                                                    |
| `last-release-channel`               | `next`   | The distribution channel on which the last release was initially made available *(`null` for the default distribution channel)* |
| `last-release-version`               | `1.0.0`  | The version of the last release                                                                                                 |
| `last-release-version-major`         | `1`      | last release version major component                                                                                            |
| `last-release-version-minor`         | `0`      | last release version minor component                                                                                            |
| `last-release-version-patch`         | `0`      | last release version patch component                                                                                            |
| `last-release-version-prerelease`    | `-`      | last release version prerelease component                                                                                       |
| `last-release-version-buildmetadata` | `-`      | last release version buildmetadata component                                                                                    |
| `release-type`                       | `major`  | The semver type of the release (patch, minor or major)                                                                          |
| `release-git-head`                   | `d494d2` | The sha of the last commit being part of the new release                                                                        |
| `release-git-tag`                    | `v1.1.0` | The Git tag associated with the new release                                                                                     |
| `release-version`                    | `1.1.0`  | The version of the new release.                                                                                                 |
| `release-notes`                      | `...`    | The release notes for the new release                                                                                           |
| `release-channel`                    | `next`   | The distribution channel on which the next release will be made available *(`null` for the default distribution channel)*       |
| `release-version-major`              | `1`      | last release version major component                                                                                            |
| `release-version-minor`              | `1`      | last release version minor component                                                                                            |
| `release-version-patch`              | `0`      | last release version patch component                                                                                            |
| `release-version-prerelease`         | `-`      | last release version prerelease component                                                                                       |
| `release-version-buildmetadata`      | `-`      | last release version buildmetadata component      


> **Note**: Ensure `portfolio_wrapper` is set to `false` to better capture the output into a file *(or use your own method)*

> Author: [João Pedro](https://joaopedro.azurewebsites.net/) &bull;
> Linkedin: [@joomoraes](https://www.linkedin.com/in/joaopedroalvesdemoraes/)
[license-url]: LICENSE
[license-img]: https://badgen.net/github/license/joomoraes/me-portfolio_mongodb_aspnet_web
