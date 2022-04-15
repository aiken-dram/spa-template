# Documentation for application

## Development

```sh
npm run serve
```

## Publishing

```sh
npm run build
```

## Deploying

Copy content of `doc` to virtual directory on IIS named `doc`

## Manual.docx

Using installed `pandoc` run in `docs/manuals` directory

```sh
pandoc user.md -f markdown -t docx -s -o manual.docx
```
