/// <binding ProjectOpened='watch:tasks' />
/*
This file in the main entry point for defining grunt tasks and using grunt plugins.
Click here to learn more. http://go.microsoft.com/fwlink/?LinkID=513275&clcid=0x409
*/
module.exports = function (grunt) {
    grunt.initConfig({
        less: {
            development: {
                options: {
                    paths: ["Content"]
                },
                files: {
                    "Content/site.css": "Content/site.less"
                }
            },
            production: {
                options: {
                    paths: ["Content"],
                },
                files: {
                    "Content/site.css": "Content/site.less"
                }
            }
        },
        watch: {
            files: ["Content/**/*.less"],
            tasks: ["less:development"]
        }
    });
    grunt.loadNpmTasks("grunt-contrib-less");
    grunt.loadNpmTasks('grunt-contrib-watch');
};