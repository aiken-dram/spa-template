export default {
  /** filter functions */
  filters: {
    /**
     * show element in filter
     * @param {*} type type of filter
     * @param {*} element name of element
     * @returns boolean value
     */
    show(type, element) {
      switch (element) {
        case "select":
          return ["text", "number"].includes(type);
        case "sign":
          return ["text", "number", "mask", "dict", "dictString"].includes(
            type
          );
        default:
          return ["text", "number", "mask", "dict", "dictString"].includes(
            type
          );
      }
    },

    /**
     * get operators for filter
     * @param {*} type type of filter
     * @returns array of operators to select
     */
    operators(type) {
      switch (type) {
        case "text":
          return ["contains", "equals", "startswith", "endswith"];
        case "number":
          return ["equals", "less", "more"];
        default:
          return null;
      }
    },

    /**
     * get initial operator for filter
     * @param {*} type type of filter
     * @returns initial operator
     */
    initial(type) {
      switch (type) {
        case "text":
          return "contains";
        case "number":
          return "equals";
      }
    },

    /**
     * Returns filter for provided parameters
     * @param {*} operator name of operator (can be null)
     * @param {*} type type of filter
     * @param {*} name name of field in database
     * @param {*} text text value
     * @param {*} number number value
     * @param {*} dateRange date range array
     * @param {*} dict dictionary value
     * @param {*} checkbox checkbox value
     * @param {*} filterFlags checkbox value
     * @param {*} flags checkbox value
     * @param {*} FLAGS checkbox value
     * @returns search string
     */
    getFilter(
      operator,
      type,
      name,
      text,
      number,
      dateRange,
      dict,
      checkbox,
      filterFlags,
      flags,
      FLAGS
    ) {
      switch (type) {
        case "text":
          return this.text(operator, name, text);
        case "number":
          return this.number(operator, name, number);
        case "date":
          return this.date(name, dateRange);
        case "mask":
          return this.equals(name, text, operator);
        case "dict":
          return this.equals(name, dict, operator);
        case "checkbox":
          return this.equals(name, checkbox);
        case "flags":
          return this.flags(filterFlags, flags, FLAGS);
        default:
          return null;
      }
    },

    /**
     * Return extended search for provided parameters
     * @param {*} operator name of operator (can be null)
     * @param {*} type type of filter
     * @param {*} name name of field in database
     * @param {*} value search value
     * @returns search string
     */
    getExtended(operator, type, name, value) {
      switch (type) {
        case "text":
          return this.text(operator, name, value);
        case "number":
          return this.number(operator, name, value);
        case "date":
          return this.equals(name, value);
        case "dict":
        case "dictString":
          return this.equals(name, value, operator);
        default:
          return null;
      }
    },

    /** text filter */
    text(operator, name, text) {
      switch (operator) {
        case "contains":
          return `${name}|like|%${text}%`;
        case "!contains":
          return `${name}|!like|%${text}%`;
        case "equals":
          return `${name}|==|${text}`;
        case "!equals":
          return `${name}|!=|${text}`;
        case "startswith":
          return `${name}|like|${text}%`;
        case "!startswith":
          return `${name}|!like|${text}%`;
        case "endswith":
          return `${name}|like|%${text}`;
        case "!endswith":
          return `${name}|like|%${text}`;
        default:
          return null;
      }
    },

    /** number filter */
    number(operator, name, number) {
      switch (operator) {
        case "equals":
          return `${name}|==|${number}`;
        case "!equals":
          return `${name}|!=|${number}`;
        case "more":
        case "!less":
          return `${name}|>=|${number}`;
        case "less":
        case "!more":
          return `${name}|<=|${number}`;
        default:
          return null;
      }
    },

    date(name, dateRange) {
      return `${name}|date|${dateRange.join(" ~ ")}`;
    },

    equals(name, text, operator) {
      if (operator && operator.startsWith("!")) return `${name}|!=|${text}`;
      else return `${name}|==|${text}`;
    },

    flags(filterFlags, flags, FLAGS) {
      return filterFlags.map((p) => {
        if (p.group)
          return `${p.group.map((q) => q.field).join(",")}|group|${flags[
            p.name
          ].join(",")}`;
        else
          return `${p.field}|in|${this.getFlag(p.name, flags[p.name], FLAGS)}`;
      });
    },

    getFlag(name, value, FLAGS) {
      //value can be undefined or array?
      var f = FLAGS[name];

      if (value && value.length > 0)
        return value.map((v) => f[v].value).join(",");

      return null;
    },
  },
};
