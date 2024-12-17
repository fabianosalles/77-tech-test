<template>
    <div class="card">
        <table>
            <caption>Book List <span>({{ count }})</span></caption>
            <thead>
                <tr>
                    <th>ISBN</th>
                    <th>Name</th>
                    <th>Author</th>
                    <th>Edition</th>
                    <th>Publisher</th>
                    <th>Description</th>
                </tr>
            </thead>
            <tbody>
                <tr v-for="book in books" :key="book.id">
                    <td>{{ book.isbn }}</td>
                    <td>{{ book.name }}</td>
                    <td>{{ book.author }}</td>
                    <td>{{ book.edition }}</td>
                    <td>{{ book.publisher }}</td>
                    <td>{{ book.description }}</td>
                </tr>
            </tbody>
        </table>
    </div>
    <section class="toolbar">
        <button type="button" v-on:click.prevent="fireNewBookEvent">New book</button>
    </section>
</template>

<script>
export default {
    name: 'BookList',
    emits: ['show-new-book-form'],
    data() {
        return {
            count: 0,
            books: []
        }
    },
    methods: {
        fireNewBookEvent() {
            this.$emit('show-new-book-form')
        },

        async reload() {
            const response = await this.getBooks(0)
            this.books = response.books;
            this.count = response.total;
        },

        async getBooks(page) {
            const pageSize = 100
            const offset = page * pageSize
            const url = `https://localhost:7273/api/v1/books?Offset=${offset}&Limit=${pageSize}`
            try {
                const response = await fetch(url, { mode: 'cors' })
                if (!response.ok) {
                    throw new Error(`Response status: ${response.status}`);
                }
                return await response.json()
            } catch (e) {
                console.error(e);
            }
        }
    },
    async created() {
        await this.reload()
    }
}
</script>

<style scoped>
.card {
    width: 80vw;
    max-width: 100rem;
    border: 2px dotted var(--color-highlight-alpha);
    margin: 0 auto;
    padding: .8rem;
    border-radius: 1rem;
}

table {
    border-collapse: collapse;
    vertical-align: middle;
    width: 100%;

    caption {
        font-weight: bold;
        font-size: 1.2rem;
        padding: .8rem;
        text-align: center;
        color: var(--color-highlight);
        background-color: var(--color-highlight-alpha);
        border-radius: .5rem .5rem 0 0;

        span {
            font-weight: normal;
        }
    }

    thead {
        background-color: var(--color-highlight);
        color: white;
    }

    tbody tr:nth-child(odd) {
        background-color: #fff;
    }

    tbody tr:nth-child(even) {
        background-color: #eeeeee96;
    }
}

th,
td {
    border: 1px solid var(--color-highlight-alpha);
    padding: .3rem;
}

th {
    text-align: center;
}

.toolbar {
    margin: 1rem;
}
</style>