<template>
  <dialog id="dialog-add-new-book">
    <form @submit.prevent="submit" id="form-book">
      <field>
        <label for="isbn" data-required>ISBN</label>
        <input type="text" id="isbn" required v-model="form.isbn" maxlength="13">
      </field>

      <field>
        <label for="name">Name</label>
        <input type="text" id="name" required v-model="form.name">
      </field>

      <field>
        <label for="author">Author</label>
        <input type="text" id="author" required v-model="form.author">
      </field>

      <field>
        <label for="edition">Edition</label>
        <input type="number" id="edition" required v-model="form.edition">
      </field>

      <field>
        <label for="publisher">Publisher</label>
        <input type="text" id="publisher" required v-model="form.publisher">
      </field>

      <field>
        <label for="description">Description</label>
        <textarea id="description" rows="5" v-model="form.description" />
      </field>

      <button v-on:click.prevent="submit">Submit</button>
      <a href="#" v-on:click.prevent="cancel">Cancel</a>
    </form>
  </dialog>
</template>

<script>
export default {
  name: 'AddBookForm',
  emits: ['new-book-added'],
  data() {
    return {
      form: {
        id: null,
        isbn: '',
        name: '',
        author: '',
        edition: 1,
        description: null,
        publisher: null
      }
    }
  },

  methods: {
    cancel() {
      document.getElementById('form-book').reset()
      document.getElementById('dialog-add-new-book').close()
    },

    showModal() {
      document.getElementById('dialog-add-new-book').showModal()
    },

    validate() {
      let result = []
      if (!this.form.isbn) {
        result.push("Isbn is required")
      }

      if (!this.form.name) {
        result.push("Name is required")
      }

      if (!this.form.author) {
        result.push("Author is required")
      }

      return result.join('\n')
    },

    async submit() {
      let messages = this.validate()
      if (messages.length > 0) {
        alert(messages)
        return
      }
      
      debugger
      const result = await this.postData()
      if (result.success) {
        this.$emit('new-book-added')
        this.cancel()
      }
      else {
        if (result.response.errors) {
          console.error(JSON.stringify(result))
          alert('Error: ' + JSON.stringify(result.response.errors))
        }
        else {
          alert('Error: ' + JSON.stringify(result))
        }
      }
    },

    async postData() {
      const url = `https://localhost:7273/api/v1/books`
      const response = await fetch(url, {
        mode: 'cors',
        method: 'POST',
        headers: {
          'Accept': 'application/json',
          'Content-Type': 'application/json'
        },
        body: JSON.stringify(this.form)
      })
      debugger
      if (response.status == 201)
        return { success: true }

      let data = await response.json()
      return { success: false, response: data }
    }
  }
}
</script>

<style scoped>
dialog {
  width: 15rem;
  border: 2px solid var(--color-highlight-alpha);
  border-radius: .5rem;
  padding: 1.5rem;
  box-shadow: 0px 0px 20px rgba(0, 0, 0, 0.123);

  form {
    field {
      display: block;
      margin-bottom: .5rem;

      label {
        font-size: small;
        font-weight: bold;
        color: var(--color-highlight);
        display: flex;
      }

      input,
      textarea {
        display: flex;
        width: 95%;
        padding: .25rem;
      }

      .error {
        font-size: small;
        color: red;
        text-align: left;
        display: block;
      }
    }

    a {
      text-decoration: none;
      margin-left: 1rem;
      font-size: small;
    }
  }
}

::backdrop {
  background-color: var(--color-highlight-alpha);
  opacity: 0.5;
}
</style>