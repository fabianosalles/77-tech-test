<template>
  <nav>
    <router-link to="/">Books</router-link> |
    <router-link to="/about">About</router-link>
  </nav>
  <router-view />
</template>

<style>
:root {
  --color-gray: #2c3e50;
  --color-highlight: #42b983;
  --color-highlight-alpha: #42b98342;
}

#app {
  font-family: Avenir, Helvetica, Arial, sans-serif;
  -webkit-font-smoothing: antialiased;
  -moz-osx-font-smoothing: grayscale;
  text-align: center;
  color: var(--color-gray);
}

nav {
  padding: 30px;
}

nav a {
  font-weight: bold;
  color: var(--color-gray);
}

nav a.router-link-exact-active {
  color: var(--color-highlight);
}

button {
  border: 2px solid var(--color-highlight-alpha);
  padding: .5rem 1rem;
  border-radius: .4rem;
  cursor: pointer;
  background-color: #eeeeee96;

  &:hover {
    background-color: var(--color-highlight-alpha);
  }
}
</style>
